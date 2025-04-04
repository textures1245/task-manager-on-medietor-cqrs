using System.Reflection.Metadata.Ecma335;

using ErrorOr;
namespace MediatrCqrs.Application.Domain.ValueObjects.Colour;

public class Colour
{
    static Colour() { }

    private Colour() { }

    private Colour(string code)
    {
        Code = code;
    }

    public static ErrorOr<Colour> From(string code)
    {
        var colour = new Colour { Code = code };

        return !SupportedColours.Contains(colour) ? ColourErrors.UnsupportedColour(code) : colour;
    }

    public static Colour White => new("#FFFFFF");

    public static Colour Red => new("#FF5733");

    public static Colour Orange => new("#FFC300");

    public static Colour Yellow => new("#FFFF66");

    public static Colour Green => new("#CCFF99 ");

    public static Colour Blue => new("#6666FF");

    public static Colour Purple => new("#9966CC");

    public static Colour Grey => new("#999999");

    public string Code { get; private set; } = "#000000";

    public static implicit operator string(Colour colour)
    {
        return colour.ToString();
    }

    public override string ToString()
    {
        return Code;
    }

    protected static IEnumerable<Colour> SupportedColours
    {
        get
        {
            yield return White;
            yield return Red;
            yield return Orange;
            yield return Yellow;
            yield return Green;
            yield return Blue;
            yield return Purple;
            yield return Grey;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }


}
