using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace macmod.Tools;

public static class ValidationErrorBuilder
{
    public static string GenerateErrorString(ModelStateDictionary modelStateDictionary)
    {
        return string.Join("; ", modelStateDictionary.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .Where(e => !string.IsNullOrWhiteSpace(e)));
    }
}