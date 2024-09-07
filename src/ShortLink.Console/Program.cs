Console.WriteLine("Генератор коротких ссылок.");
Console.Write("Вставьте вашу ссылку: ");
string url = Console.ReadLine()!;

var service = new ShortLinkService("https://localhost:5200");
var shortLink = await service.PostLinkAsync(url);
if (shortLink == null)
{
    return;
}
Console.WriteLine($"Успех!\n" +
    $"Сокращенная ссылка: {shortLink!.ShortUrl}\n" +
    $"Оригинальаня ссылка: {shortLink.OriginalUrl}\n" +
    $"Количество переходов: {shortLink.Views}.\n");