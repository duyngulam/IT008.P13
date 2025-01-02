using Newtonsoft.Json;
using System.IO;

public static class ScoreSaveManager
{
    private static readonly string SaveDirectory = "Score";

    static ScoreSaveManager()
    {
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }
    }

    // Lưu trạng thái game vào slot
    public static void SaveToSlot(int slot, string saveData)
    {
        string filePath = GetSlotFilePath(slot);

        // Đọc dữ liệu cũ từ file nếu có
        string existingData = "";
        if (File.Exists(filePath))
        {
            existingData = File.ReadAllText(filePath);
        }

        // Deserialize dữ liệu cũ nếu có, hoặc tạo một danh sách mới nếu không có dữ liệu
        var savedList = new List<string>();

        // Kiểm tra nếu dữ liệu cũ không trống và là JSON hợp lệ
        if (!string.IsNullOrEmpty(existingData))
        {
            try
            {
                // Kiểm tra xem dữ liệu cũ có phải là một danh sách chuỗi không
                savedList = JsonConvert.DeserializeObject<List<string>>(existingData);
            }
            catch (JsonException)
            {
                // Nếu có lỗi trong việc deserialization, ghi lại dữ liệu cũ dưới dạng danh sách mới
                savedList = new List<string>();
            }
        }

        // Thêm dữ liệu mới vào danh sách
        savedList.Add(saveData);

        // Serialize lại và ghi vào file
        File.WriteAllText(filePath, JsonConvert.SerializeObject(savedList, Formatting.Indented));
    }

    // Tải trạng thái game từ slot
    public static List<string> LoadFromSlot(int slot)
    {
        string filePath = GetSlotFilePath(slot);

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Slot {slot} chưa có dữ liệu!");

        // Đọc dữ liệu từ file và deserializing thành List<string>
        string fileContent = File.ReadAllText(filePath);

        try
        {
            // Deserialize nội dung file thành List<string>
            return JsonConvert.DeserializeObject<List<string>>(fileContent);
        }
        catch (JsonException)
        {
            // Nếu có lỗi khi deserializing, trả về một danh sách rỗng
            return new List<string>();
        }
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
