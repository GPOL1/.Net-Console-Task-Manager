using System.Text.Json;

//Interface ensures that classes contain all required functions.
public interface ITaskList{
    List<TaskItem> tasks { get; }
    Task addTask(TaskItem task, string file);
    void viewTaskId(int _id);
    void viewTasks();
    void editTask(int _id, string _title, DateTime _dueDate, bool _isCompleted);
    void deleteTask(int _id);
    bool hasId(int _id);
    int GetLastTaskId();
    void filterTasks(int _id);
    Task saveToJson(string filePath);
    Task loadFromJson(string filePath);
}

//Holds all information for all functions that interact with Tasklist.
public class TaskList : ITaskList{

    public List<TaskItem> tasks{ get; set;} = new();

    public async Task addTask(TaskItem task, string file){
        tasks.Add(task);
        await saveToJson(file);
    }

    //View the task with a specific id.
    public void viewTaskId(int _id){
        TaskItem task = tasks.FirstOrDefault(t => t.id == _id);
        Console.WriteLine($"{task.id} - {task.title} - Due: {task.dueDate:d} - Completed: {task.isCompleted}");
    }
    
    //View all tasks
    public void viewTasks(){
        Console.WriteLine("Showing all current tasks!");
        tasks.ForEach(task => Console.WriteLine($"{task.id} - {task.title} - Due: {task.dueDate:d} - Completed: {task.isCompleted}"));
    }

    //Only edits properties with new information to edit.
    public void editTask(int _id, string _title, DateTime _dueDate, bool _isCompleted){
        TaskItem task = tasks.FirstOrDefault(t => t.id == _id);
        if (_title != ""){
            task.title = _title;
        }
        if (_dueDate != new DateTime()){
            task.dueDate = _dueDate;
        }
        task.isCompleted = _isCompleted;
    }

    public void deleteTask(int _id){
        TaskItem task = tasks.FirstOrDefault(t => t.id == _id);
        tasks.Remove(task);
    }

    //Checks if id exists in current Tasklist
    public bool hasId(int _id){
        TaskItem task = tasks.FirstOrDefault(t => t.id == _id);
        if (task == null){
            return false;
        }
        viewTaskId(task.id);
        return true;
    }
    
    public int GetLastTaskId()
    {
        return tasks.Last().id;
    }
    public void filterTasks(int _id){
        List<TaskItem> sortedList = new();
        switch(_id){
            case 1:
                sortedList = tasks.OrderBy(o=>o.isCompleted).ToList();
                break;
            case 2:
                sortedList = tasks.OrderByDescending(o=>o.isCompleted).ToList();
                break;
            case 3:
                sortedList = tasks.OrderBy(o=>o.dueDate).ToList();
                break;
            case 4:
                sortedList = tasks.OrderByDescending(o=>o.dueDate).ToList();
                break;
            case 5:
                sortedList = tasks.OrderBy(o=>o.title).ToList();
                break;
            case 6:
                sortedList = tasks.OrderByDescending(o=>o.title).ToList();
                break;
            default:
                Console.WriteLine("An error occured!");
                return;
        }
        Console.WriteLine("Showing sorted tasks!");
        sortedList.ForEach(task => Console.WriteLine($"{task.id} - {task.title} - Due: {task.dueDate:d} - Completed: {task.isCompleted}"));
        Console.WriteLine();
    }

    public async Task saveToJson(string filePath){
        var json = JsonSerializer.Serialize(tasks);
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task loadFromJson(string filePath){
        if (File.Exists(filePath)){
            var json = await File.ReadAllTextAsync(filePath);
            tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }
    }
}