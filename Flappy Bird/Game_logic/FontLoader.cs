// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Drawing.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class FontLoader
{
    // Здесь будут храниться все загруженные шрифты
    public static PrivateFontCollection PFC = new PrivateFontCollection();

    /// <summary>
    /// Загружает встроенный ресурс-шрифт по его имени.
    /// </summary>
    public static void LoadFont(string resourceName)
    {
        // Получаем сборку (нашу программу), где лежат ресурсы
        var assembly = Assembly.GetExecutingAssembly();

        // Пытаемся открыть ресурс как поток данных
        var stream = assembly.GetManifestResourceStream(resourceName);

        // Если шрифт не найден — выдаём ошибку
        if (stream == null)
            throw new Exception("Шрифт не найден: " + resourceName);

        // Создаём массив нужного размера и читаем шрифт в память
        byte[] fontData = new byte[stream.Length];
        stream.Read(fontData, 0, fontData.Length);

        // Выделяем область памяти в Windows под этот шрифт
        IntPtr memory = Marshal.AllocCoTaskMem(fontData.Length);

        // Копируем байты шрифта в выделенную память
        Marshal.Copy(fontData, 0, memory, fontData.Length);

        // Добавляем шрифт в коллекцию, чтобы использовать его в программе
        PFC.AddMemoryFont(memory, fontData.Length);

        // Освобождаем выделенную память
        Marshal.FreeCoTaskMem(memory);
    }
}