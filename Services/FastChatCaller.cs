using System.Diagnostics;

namespace FastChat.Services;

public class FastChatCaller
{
    public class RouteVm
    {
        public string Name
        {
            get; set;
        }

        public string Url
        {
            get; set;
        }
    }

    public Dictionary<int, RouteVm> Vm
    {
        get; set;
    } = new()
    {
        {1, new RouteVm { Name = "Государственные закупки", Url = "http://192.168.162.134:7129/en/prava/prava3" } },
        {2, new RouteVm { Name = "Права", Url = "http://192.168.162.134:7129/en/prava" } },
        {3, new RouteVm { Name = "Власть", Url = "http://192.168.162.134:7129/en/vlast" } },
        {4, new RouteVm { Name = "Инструкции", Url = "http://192.168.162.134:7129/en/instruction" } },
        {5, new RouteVm { Name = "Миграция", Url = "http://192.168.162.134:7129/en/prava/prava6" } },
        {6, new RouteVm { Name = "Налоги", Url = "http://192.168.162.134:7129/en/prava/prava7" } },
        {7, new RouteVm { Name = "Админресурс", Url = "http://192.168.162.134:7129/en/vlast/vlast1" } },
        {8, new RouteVm { Name = "Госзакупки", Url = "http://192.168.162.134:7129/en/vlast/vlast2" } },
        {9, new RouteVm { Name = "Госуслуги", Url = "http://192.168.162.134:7129/en/vlast/vlast3" } },
        {10, new RouteVm { Name = "Декларации", Url = "http://192.168.162.134:7129/en/vlast/vlast4" } },
        {11, new RouteVm { Name = "Гражданское участие", Url = "http://192.168.162.134:7129/en/vlast/vlast2" } },
        {12, new RouteVm { Name = "Конфликт интересов", Url = "http://192.168.162.134:7129/en/instruction/instruction3" } },
        {13, new RouteVm { Name = "Доступ к информации", Url = "http://192.168.162.134:7129/en/instruction/instruction2" } },
        {14, new RouteVm { Name = "ГРС:Личные документы", Url = "http://192.168.162.134:7129/en/prava/prava2" } },
        {15, new RouteVm { Name = "Земельная сфера", Url = "http://192.168.162.134:7129/en/prava/prava4" } },
        {16, new RouteVm { Name = "Здравоохранение", Url = "http://192.168.162.134:7129/en/prava/prava5" } },
        {17, new RouteVm { Name = "Образование", Url = "http://192.168.162.134:7129/en/prava/prava8" } },
        {18, new RouteVm { Name = "Армия", Url = "http://192.168.162.134:7129/en/prava/prava1" } }
    };



    public RouteVm SendQuestion(string question)
    {
        var process = new Process();
        var startInfo = new ProcessStartInfo();

        // Задайте путь к исполняемому файлу Python (обычно python или python3)
        startInfo.FileName = "python";

        // Укажите путь к вашему скрипту Python
        startInfo.Arguments = $"chat.py \"{question}\"";

        // Установите параметры для перенаправления вывода
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;

        process.StartInfo = startInfo;
        process.Start();

        // Прочитайте результат вывода
        var output = process.StandardOutput.ReadToEnd();

        process.WaitForExit();

        var index = int.Parse(output);

        return Vm[index];
    }
}
