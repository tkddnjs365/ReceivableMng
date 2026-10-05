using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ReceivableMng.Controls
{
    /// <summary>
    /// 여러 업체를 체크박스로 선택할 수 있는 콤보박스 형태의 사용자 정의 컨트롤이다.
    /// 체크된 업체가 여러 개일 경우 선택된 업체 수를 버튼에 표시한다.
    /// </summary>
    public partial class CheckedComboBox : UserControl
    {
        // 팝업으로 사용할 ToolStripDropDown (폼 대신 사용하여 포커스/활성화 문제 해결)
        private readonly ToolStripDropDown _popup;

        // 팝업 안에서 업체를 체크할 수 있는 목록
        private readonly CheckedListBox _list;

        // 현재 콤보박스에서 사용하는 업체 목록
        // Id : 업체를 구분하는 고유 ID
        // Text : 화면에 표시할 업체명
        private List<(int Id, string Text)> _items = new();

        /// <summary>
        /// 업체 선택 상태가 변경되었을 때 발생하는 이벤트이다.
        /// </summary>
        public event EventHandler SelectionChanged;

        public CheckedComboBox()
        {
            InitializeComponent();

            // 콤보박스 버튼을 클릭하면 업체 선택 팝업을 열거나 닫는다.
            _btnToggle.Click += (s, e) => TogglePopup();

            // 업체를 여러 개 선택할 수 있는 체크 목록을 생성한다.
            _list = new CheckedListBox
            {
                CheckOnClick = true,
                BorderStyle = BorderStyle.FixedSingle,
                IntegralHeight = false,
                BackColor = Color.White
            };

            // 업체의 체크 상태가 변경될 때 실행되는 이벤트를 연결한다.
            _list.ItemCheck += List_ItemCheck;

            // ToolStripDropDown을 팝업으로 사용한다. ToolStripControlHost로 CheckedListBox를 호스트하면
            // 클릭 외부 시 자동으로 닫히고 메인 폼의 활성화 순서가 꼬이는 문제를 방지한다.
            var host = new ToolStripControlHost(_list)
            {
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                AutoSize = false
            };

            _popup = new ToolStripDropDown
            {
                Padding = Padding.Empty,
                Margin = Padding.Empty
            };
            // 기본 크기와 호스트 크기 설정
            _list.Size = new Size(220, 220);
            host.Size = _list.Size;
            _popup.Items.Add(host);
        }

        /// <summary>
        /// 업체의 체크 상태가 변경되었을 때 실행된다.
        /// </summary>
        private void List_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // ItemCheck 이벤트는 체크 상태가 실제로 변경되기 전에 발생한다.
            // 따라서 현재 이벤트가 끝난 다음 최종 체크 상태를 확인해야 한다.
            BeginInvoke(new Action(() =>
            {
                // 현재 선택된 업체 수에 맞게 버튼의 표시 문구를 변경한다.
                UpdateLabel();

                // 업체 선택 상태가 변경되었다는 것을 외부에 알린다.
                SelectionChanged?.Invoke(this, EventArgs.Empty);
            }));
        }

        /// <summary>
        /// 업체 선택 팝업을 열거나 닫는다.
        /// </summary>
        private void TogglePopup()
        {
            // 팝업이 이미 열려 있다면 팝업을 닫는다.
            if (_popup.Visible)
            {
                _popup.Close();
                return;
            }

            // 호스트 컨트롤(이 컨트롤) 기준으로 팝업을 아래에 표시한다.
            // ToolStripDropDown.Show(Control, Point)은 컨트롤의 Z-order와 활성화 문제를 안전하게 처리한다.
            _popup.Show(this, new Point(0, Height));
        }

        /// <summary>
        /// 드롭다운에 표시할 업체 목록을 설정한다.
        /// 기존에 선택되어 있던 업체는 가능한 경우 선택 상태를 유지한다.
        /// </summary>
        public void SetItems(IEnumerable<(int Id, string Text)> items)
        {
            // 현재 체크되어 있는 업체 ID를 먼저 저장한다.
            // 업체 목록을 다시 설정한 후에도 기존 선택 상태를 유지하기 위해 사용한다.
            var previousCheckedIds = GetCheckedIds();

            // 전달받은 업체 목록을 List로 변환하여 저장한다.
            _items = items.ToList();

            // 기존 업체 목록을 모두 제거한다.
            _list.Items.Clear();

            // 새로운 업체 목록을 체크박스 목록에 추가한다.
            foreach (var item in _items)
            {
                _list.Items.Add(item.Text);
            }

            // 이전에 선택되어 있던 업체가 새로운 목록에도 존재한다면
            // 해당 업체를 다시 체크한다.
            for (int i = 0; i < _items.Count; i++)
            {
                if (previousCheckedIds.Contains(_items[i].Id))
                {
                    _list.SetItemChecked(i, true);
                }
            }

            // 현재 선택된 업체 수에 맞게 버튼의 표시 문구를 변경한다.
            UpdateLabel();
        }

        /// <summary>
        /// 현재 체크되어 있는 업체의 ID 목록을 반환한다.
        /// 아무 업체도 체크하지 않은 경우 빈 HashSet을 반환하며 전체 업체를 의미한다.
        /// </summary>
        public HashSet<int> GetCheckedIds()
        {
            // 체크된 업체 ID를 저장할 HashSet을 생성한다.
            // HashSet은 동일한 ID가 중복으로 들어가는 것을 방지한다.
            var checkedIds = new HashSet<int>();

            // CheckedListBox에서 현재 체크된 항목의 인덱스를 하나씩 가져온다.
            foreach (int index in _list.CheckedIndices)
            {
                // 목록의 인덱스를 이용하여 실제 업체 ID를 가져온다.
                checkedIds.Add(_items[index].Id);
            }

            // 체크되어 있는 업체 ID 목록을 반환한다.
            return checkedIds;
        }

        /// <summary>
        /// 현재 선택되어 있는 모든 업체의 체크 상태를 해제한다.
        /// </summary>
        public void ClearSelection()
        {
            // 현재 목록에 있는 모든 항목을 확인한다.
            for (int i = 0; i < _list.Items.Count; i++)
            {
                // 해당 업체의 체크 상태를 해제한다.
                _list.SetItemChecked(i, false);
            }

            // 선택된 업체가 없는 상태에 맞게 버튼의 표시 문구를 변경한다.
            UpdateLabel();
        }

        /// <summary>
        /// 현재 선택된 업체 수에 따라 버튼에 표시되는 문구를 변경한다.
        /// </summary>
        private void UpdateLabel()
        {
            // 현재 체크되어 있는 업체의 개수를 가져온다.
            var checkedCount = _list.CheckedItems.Count;

            // 선택된 업체가 없으면 "전체 업체"를 표시한다.
            // 업체가 하나만 선택되면 해당 업체명을 표시한다.
            // 여러 업체가 선택되면 선택된 업체의 개수를 표시한다.
            _btnToggle.Text = checkedCount == 0
                ? "전체 업체"
                : checkedCount == 1
                    ? _list.CheckedItems[0].ToString()
                    : $"{checkedCount}개 업체 선택";
        }
    }
}