
using ErrorOr;

namespace MediatrCqrs.Application.Domain.ValueObjects.Colour;

public class ColourErrors
{
    public static Error UnsupportedColour(string code) =>
    Error.Validation(code: "ColourErrors.UnsupportedColour", description: $"The colour code '{code}' is not supported");
}
