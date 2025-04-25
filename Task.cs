public class Task{

    public static int counter = 1;

    public int id { get; set;}
    public string title { get; set;} = "";
    public DateTime dueDate { get; set;}
    public bool isCompleted { get; set;}

    public Task(){
        id = counter++;
        isCompleted = false;
    }

}