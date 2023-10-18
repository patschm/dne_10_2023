namespace Calculator;

public partial class CalculatorApp : Form
{
    private SynchronizationContext? t0;

    public CalculatorApp()
    {
        InitializeComponent();
        t0 = SynchronizationContext.Current;
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        if (int.TryParse(txtA.Text, out int a) && int.TryParse(txtB.Text, out int b)) 
        {
            //int result = LongAdd(a, b);
            //UpdateAnswer(result);

            //Task.Run(() => LongAdd(a, b))
            //    .ContinueWith(pTask => {
            //        t0.Send(UpdateAnswer, pTask.Result);
            //        //UpdateAnswer(pTask.Result);
            //    });

            DoJob(a,b).Wait();
        }      
    }

    public async Task DoJob(int a, int b)
    {
        var result = await LongAddAsync(a, b);//.ConfigureAwait(true);
        UpdateAnswer(result);
    }

    private void UpdateAnswer(object? result)
    {
        lblAnswer.Text = result?.ToString();
    }

    private int LongAdd(int a, int b)
    {
        Task.Delay(10000).Wait();
        return a + b;
    }
    private Task<int> LongAddAsync(int a, int b)
    {
        return Task.Run(() => LongAdd(a, b));
    }
}