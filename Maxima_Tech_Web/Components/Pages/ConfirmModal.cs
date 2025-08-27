using Microsoft.AspNetCore.Components;

namespace Maxima_Tech_Web.Components.Pages
{
    public partial class ConfirmModal
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment FooterContent { get; set; }

        [Parameter]
        public bool IsOpen { get; set; }
        [Parameter]
        public int Width { get; set; }
        [Parameter]
        public int Height { get; set; }
        private string Message = "";
        private string Size => Width > 0 && Height > 0 ? $"width: {Width}px;height: {Height}px; " : "";
        private string ModalStyle => IsOpen ? "display: block;" : "display: none;";
        public void CloseModal()
        {
            IsOpen = false;
        }
    }
}
