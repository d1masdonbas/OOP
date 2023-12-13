using System;
using System.Collections;
using System.Collections.Generic;

public enum TimeFrame
{
    Year,
    TwoYears,
    Long
}

public interface INameAndCopy
{
    string Name { get; set; }
    object DeepCopy();
}

public class Person : INameAndCopy
{
    public Person()
    {
            
    }
    public string Name { get; set; }

    public bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Person otherPerson = (Person)obj;
        return Name == otherPerson.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public virtual object DeepCopy()
    {
        return new Person { Name = this.Name };
    }

    public Person(Person person)
    {
        Name = person.Name;  
    }
}

public class Paper : INameAndCopy
{
    public string Title { get; set; }
    public Person Author { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Name { get; set; }

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

    public virtual object DeepCopy()
    {
        return new Paper { Title = Title, Author = (Person)Author.DeepCopy(), PublicationDate = PublicationDate };
    }
}

public class Team : INameAndCopy
{
    protected string organization;
    private int registrationNumber;

    public string Name { get; set; }

    public Team(string organization, int registrationNumber)
    {
        this.organization = organization;
        this.RegistrationNumber = registrationNumber;
    }

    public Team() { }

    public string Organization
    {
        get { return organization; }
        set { organization = value; }
    }

    public int RegistrationNumber
    {
        get { return registrationNumber; }
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("Registration number must be greater than 0.");
            registrationNumber = value;
        }
    }

    public virtual object DeepCopy()
    {
        return new Team { Organization = Organization, RegistrationNumber = RegistrationNumber };
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Team otherTeam = (Team)obj;
        return Organization == otherTeam.Organization && RegistrationNumber == otherTeam.RegistrationNumber;
    }

    public override int GetHashCode()
    {
        return Organization.GetHashCode() ^ RegistrationNumber.GetHashCode();
    }

    public override string ToString()
    {
        return $"Organization: {Organization}\nRegistration Number: {RegistrationNumber}";
    }
}

public class ResearchTeam : Team, IEnumerable<Person>, INameAndCopy
{
    private string researchTopic;
    private TimeFrame timeFrame;
    private List<Person> members;
    private List<Paper> publications;

    public ResearchTeam(string organization, int registrationNumber, string researchTopic, TimeFrame timeFrame)
        : base(organization, registrationNumber)
    {
        this.researchTopic = researchTopic;
        this.timeFrame = timeFrame;
        this.members = new List<Person>();
        this.publications = new List<Paper>();
    }

    public ResearchTeam() : base()
    {
        this.members = new List<Person>();
        this.publications = new List<Paper>();
    }

    public List<Person> Members
    {
        get { return members; }
        set { members = value; }
    }

    public List<Paper> Publications
    {
        get { return publications; }
        set { publications = value; }
    }

    public Paper LatestPublication
    {
        get
        {
            if (publications.Count == 0)
                return null;

            Paper latestPaper = publications[0];

            foreach (Paper paper in publications)
            {
                if (paper.PublicationDate > latestPaper.PublicationDate)
                    latestPaper = paper;
            }

            return latestPaper;
        }
    }

    public void AddPapers(params Paper[] newPapers)
    {
        publications.AddRange(newPapers);
    }

    public void AddMembers(params Person[] newMembers)
    {
        members.AddRange(newMembers);
    }

    public override object DeepCopy()
    {
        ResearchTeam copiedTeam = new ResearchTeam
        {
            organization = organization,
            RegistrationNumber = RegistrationNumber,
            researchTopic = researchTopic,
            timeFrame = timeFrame
        };

        foreach (Person member in members)
        {
            copiedTeam.members.Add((Person)member.DeepCopy());
        }

        foreach (Paper publication in publications)
        {
            copiedTeam.publications.Add((Paper)publication.DeepCopy());
        }

        return copiedTeam;
    }

    public IEnumerator<Person> GetEnumerator()
    {
        foreach (Person member in members)
        {
            if (HasPublications(member))
                yield return member;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private bool HasPublications(Person member)
    {
        foreach (Paper publication in publications)
        {
            if (publication.Author.Equals(member))
                return true;
        }
        return false;
    }

    public IEnumerable<Paper> GetEnumerator(int lastNYears)
    {
        foreach (Paper publication in publications)
        {
            if (publication.PublicationDate.Year >= DateTime.Now.Year - lastNYears)
                yield return publication;
        }
    }

    public override string ToString()
    {
        string result = base.ToString() + $"\nResearch Topic: {researchTopic}\nTime Frame: {timeFrame}\nMembers:\n";

        foreach (Person member in members)
        {
            result += $"{member.Name}\n";
        }

        result += "Publications:\n";

        foreach (Paper publication in publications)
        {
            result += $"{publication}\n";
        }

        return result;
    }

    public string ToShortString()
    {
        return base.ToString() + $"\nResearch Topic: {researchTopic}\nTime Frame: {timeFrame}";
    }

    public Team Team
    {
        get { return new Team(Organization, RegistrationNumber); }
        set
        {
            Organization = value.Organization;
            RegistrationNumber = value.RegistrationNumber;
        }
    }
}

class Program
{
    static void Main()
    {
        // 1. Create two Team objects with matching data and check object equality.
        Team team1 = new Team("Org1", 1);
        Team team2 = new Team("Org1", 1);

        Console.WriteLine($"Team1 == Team2: {team1 == team2}");
        Console.WriteLine($"Team1.Equals(Team2): {team1.Equals(team2)}");
        Console.WriteLine($"HashCode Team1: {team1.GetHashCode()}");
        Console.WriteLine($"HashCode Team2: {team2.GetHashCode()}\n");

        // 2. Try to assign incorrect values to the registration number property.
        try
        {
            team1.RegistrationNumber = -1;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }

        // 3. Create a ResearchTeam object, add elements to the publications and members lists, and display the data.
        ResearchTeam researchTeam = new ResearchTeam("Org2", 2, "AI Research", TimeFrame.Long);
        researchTeam.AddMembers(new Person { Name = "John Doe" }, new Person { Name = "Jane Smith" });
        researchTeam.AddPapers(new Paper("AI and Ethics", new Person { Name = "John Doe" }, DateTime.Now),
                                new Paper("Advancements in Neural Networks", new Person { Name = "Jane Smith" }, DateTime.Now.AddMonths(-6)));

        Console.WriteLine("Research Team Information:\n" + researchTeam.ToString() + "\n");

        // 4. Display the Team property for the ResearchTeam object.
        Console.WriteLine("Research Team's Team Information:\n" + researchTeam.Team.ToString() + "\n");

        // 5. Create a deep copy of the ResearchTeam object, modify the data in the original object, and display both the copy and the original object. The copied object should remain unchanged.
        ResearchTeam copiedResearchTeam = (ResearchTeam)researchTeam.DeepCopy();
        researchTeam.Organization = "New Organization";
        researchTeam.RegistrationNumber = 3;
        researchTeam.Members.Add(new Person { Name = "New Member" });
        researchTeam.Publications.Add(new Paper("New Paper", new Person { Name = "New Author" }, DateTime.Now));

        Console.WriteLine("Original Research Team Information:\n" + researchTeam.ToString() + "\n");
        Console.WriteLine("Copied Research Team Information:\n" + copiedResearchTeam.ToString() + "\n");

        // 6. Use the foreach loop for the iterator defined in the ResearchTeam class to display a list of project participants with publications.
        Console.WriteLine("Project Participants with Publications:");
        foreach (Person participant in researchTeam)
        {
            Console.WriteLine(participant.Name);
        }
        Console.WriteLine();

        // 7. Use the foreach loop for the iterator with a parameter defined in the ResearchTeam class to display a list of publications released in the last two years.
        Console.WriteLine("Publications in the Last Two Years:");
        foreach (Paper publication in researchTeam.GetEnumerator(2))
        {
            Console.WriteLine(publication);
        }
    }
}
