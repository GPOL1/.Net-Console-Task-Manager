//Holds information about each individual tasks.
public class TaskItem{

    public static int counter = 1;
    public int id { get; set;}
    public string title { get; set;} = "";
    public DateTime dueDate { get; set;}
    public bool isCompleted { get; set;} = false;

    //Used to increment id counter upon creation of every new Task.
    public TaskItem(){
        id = counter++;
    }

}