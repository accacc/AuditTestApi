using Microsoft.JSInterop;

namespace AuditUI.Client
{
    public class Helper
    {
        private readonly IJSRuntime JSRuntime;

        public Helper(IJSRuntime jsRuntime)
        {
            JSRuntime = jsRuntime;
        }

      

        public async Task StartSingleLuckyDrawResult()
        {
            
            await JSRuntime.InvokeVoidAsync("setSingleWinner", "winnerWwid", "winnerName","111", "aaaaaaa");
            
        }

        public async Task RunNormalAnimation()
        {
            await JSRuntime.InvokeVoidAsync("runNormalAnimation", null);
        }

        public async Task RunDrawAnimation()
        {
            await JSRuntime.InvokeVoidAsync("runDrawAnimation", null);
        }
    }
}
