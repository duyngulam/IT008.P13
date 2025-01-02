using Newtonsoft.Json;
using System.IO;

public static class GameSaveManager
{
    private static readonly string SaveDirectory = "Saves";

    static GameSaveManager()
    {
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }
    }

    // Lưu trạng thái game vào slot
    public static void SaveToSlot(int slot, GameSaveData saveData)
    {
        string filePath = GetSlotFilePath(slot);
        File.WriteAllText(filePath, JsonConvert.SerializeObject(saveData, Formatting.Indented));
    }

    // Tải trạng thái game từ slot
    public static GameSaveData LoadFromSlot(int slot)
    {
        string filePath = GetSlotFilePath(slot);
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Slot {slot} chưa có dữ liệu!");

        return JsonConvert.DeserializeObject<GameSaveData>(File.ReadAllText(filePath));
    }

    // Kiểm tra xem slot có dữ liệu không
    public static bool IsSlotOccupied(int slot)
    {
        return File.Exists(GetSlotFilePath(slot));
    }

    // Trả về đường dẫn file theo slot
    private static string GetSlotFilePath(int slot)
    {
        return Path.Combine(SaveDirectory, $"Slot{slot}.json");
    }
}
