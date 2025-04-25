public class TaskList{

    public List<Task> tasks{ get; set;} = new();

    public void addTask(Task task){
        tasks.Add(task);
    }

    public void viewTaskId(int _id){
        Task task = tasks.FirstOrDefault(t => t.id == _id);
        Console.WriteLine($"{task.id} - {task.title} - Due: {task.dueDate:d} - Completed: {task.isCompleted}");
    }
    
    public void viewTasks(){
        tasks.ForEach(task => Console.WriteLine($"{task.id} - {task.title} - Due: {task.dueDate:d} - Completed: {task.isCompleted}"));
    }

    public void editTask(int _id, string _title, DateTime _dueDate, bool _isCompleted){
        Task task = tasks.FirstOrDefault(t => t.id == _id);
        task.title = _title;
        task.dueDate = _dueDate;
        task.isCompleted = _isCompleted;
    }

    public void deleteTask(int _id){
        Task task = tasks.FirstOrDefault(t => t.id == _id);
        tasks.Remove(task);
    }

    public bool hasId(int _id){
        bool check = true;
        try{
            Task task = tasks.FirstOrDefault(t => t.id == _id);
        }
        catch(Exception e){
            check = false;
        }
        return check;
    }

}