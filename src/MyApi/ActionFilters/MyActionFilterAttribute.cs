using System.Collections;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyApi.Attributes;

namespace MyApi.ActionFilters;

public class MyActionFilterAttribute : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is OkObjectResult actionResult && (actionResult.Value?.GetType().IsClass ?? false))
        {
            if (typeof(IEnumerable).IsAssignableFrom(actionResult.Value!.GetType()))
            {
                var objectAsArray = (IEnumerable<object?>) actionResult.Value!;
                foreach (var obj in objectAsArray!.Where(x => x?.GetType().IsClass ?? false))
                {
                    PrintPropertyIfHasMyAttribute(obj);
                }
            }
            else
            {
                PrintPropertyIfHasMyAttribute(actionResult.Value!);
            }
        }
        Console.WriteLine("Executed");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var classObjects = context.ActionArguments
            .Where(x => x.Value?.GetType().IsClass ?? false)
            .Select(x => x.Value);

        foreach (var classObject in classObjects)
        {
            PrintPropertyIfHasMyAttribute(classObjects);
        }
        Console.WriteLine("Executing");
    }

    private void PrintPropertyIfHasMyAttribute(object? classObject)
    {
        var properties = classObject!.GetType().GetProperties();
        foreach (var property in properties)
        {
            var myAttribute = property.GetCustomAttribute<MyAttribute>();
            if (myAttribute != null)
            {
                Console.WriteLine($"{property.Name} has MyAttribute");
            }
        }
    }
}