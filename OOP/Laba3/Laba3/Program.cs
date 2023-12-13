using System;

public enum TimeFrame
{
    Year,
    TwoYears,
    Long
}

public class Person
{
    public string Name { get; set; }
}

public class Paper
{
    public string Title { get; set; }
    public Person Author { get; set; }
    public DateTime PublicationDate { get; set; }

    public Paper(string title, Person author, DateTime publicationDate)
    {
        Title = title;
        Author = author;
        PublicationDate = publicationDate;
    }

    public Paper() { }

    public override string ToString()
    {
        return $"{Title} by {Author.Name}, published on {PublicationDate.ToShortDateString()}";
    }
}

public class ResearchTeam
{
    private string researchTopic;
    private string organization;
    private int registrationNumber;
    private TimeFrame timeFrame;
    private Paper[] papers;

    public ResearchTeam(string researchTopic, string organization, int registrationNumber, TimeFrame timeFrame)
    {
        this.researchTopic = researchTopic;
        this.organization = organization;
        this.registrationNumber = registrationNumber;
        this.timeFrame = timeFrame;
        this.papers = new Paper[0];
    }

    public ResearchTeam() { }

    public string ResearchTopic
    {
        get { return researchTopic; }
        set { researchTopic = value; }
    }

    public string Organization
    {
        get { return organization; }
        set { organization = value; }
    }

    public int RegistrationNumber
    {
        get { return registrationNumber; }
        set { registrationNumber = value; }
    }

    public TimeFrame TimeFrame
    {
        get { return timeFrame; }
        set { timeFrame = value; }
    }

    public Paper[] Papers
    {
        get { return papers; }
        set { papers = value; }
    }

    public Paper LatestPublication
    {
        get
        {
            if (papers.Length == 0)
                return null;

            Paper latestPaper = papers[0];

            foreach (Paper paper in papers)
            {
                if (paper.PublicationDate > latestPaper.PublicationDate)
                    latestPaper = paper;
            }

            return latestPaper;
        }
    }

    public bool this[TimeFrame index]
    {
        get { return timeFrame == index; }
    }

    public void AddPapers(params Paper[] newPapers)
    {
        Array.Resize(ref papers, papers.Length + newPapers.Length);
        int index = papers.Length - newPapers.Length;

        foreach (Paper paper in newPapers)
        {
            papers[index] = paper;
            index++;
        }
    }

    public override string ToString()
    {
        string result = $"Research Topic: {ResearchTopic}\nOrganization: {Organization}\nRegistration Number: {RegistrationNumber}\nTime Frame: {TimeFrame}\nPublications:\n";

        foreach (Paper paper in Papers)
        {
            result += paper.ToString() + "\n";
        }

        return result;
    }

    public virtual string ToShortString()
    {
        return $"Research Topic: {ResearchTopic}\nOrganization: {Organization}\nRegistration Number: {RegistrationNumber}\nTime Frame: {TimeFrame}";
    }
}

class Program
{
    static void Main()
    {

        ResearchTeam team = new ResearchTeam("Machine Learning", "AI Research Lab", 123, TimeFrame.TwoYears);
        Console.WriteLine("Research Team Information:\n" + team.ToShortString() + "\n");

        Console.WriteLine($"TimeFrame.Year: {team[TimeFrame.Year]}");
        Console.WriteLine($"TimeFrame.TwoYears: {team[TimeFrame.TwoYears]}");
        Console.WriteLine($"TimeFrame.Long: {team[TimeFrame.Long]}\n");

       
        team.ResearchTopic = "Artificial Intelligence";
        team.Organization = "AI Research Institute";
        team.RegistrationNumber = 456;
        team.TimeFrame = TimeFrame.Year;
        Console.WriteLine("Updated Research Team Information:\n" + team.ToString() + "\n");


        Paper paper1 = new Paper("AI and Ethics", new Person { Name = "John Doe" }, DateTime.Parse("2023-01-01"));
        Paper paper2 = new Paper("Advancements in Neural Networks", new Person { Name = "Jane Smith" }, DateTime.Parse("2023-02-15"));
        team.AddPapers(paper1, paper2);
        Console.WriteLine("Updated Research Team Information with Publications:\n" + team.ToString() + "\n");


        Console.WriteLine("Publication with the Latest Date:\n" + team.LatestPublication?.ToString() + "\n");

        int numberOfElements = 1000000;

        Paper[] oneDimensionalArray = new Paper[numberOfElements];
        for (int i = 0; i < numberOfElements; i++)
        {
            oneDimensionalArray[i] = new Paper();
        }

        Paper[,] twoDimensionalRectangularArray = new Paper[1000, 1000];
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                twoDimensionalRectangularArray[i, j] = new Paper();
            }
        }

        Paper[][] twoDimensionalJaggedArray = new Paper[1000][];
        for (int i = 0; i < 1000; i++)
        {
            twoDimensionalJaggedArray[i] = new Paper[1000];
            for (int j = 0; j < 1000; j++)
            {
                twoDimensionalJaggedArray[i][j] = new Paper();
            }
        }
    }
}
