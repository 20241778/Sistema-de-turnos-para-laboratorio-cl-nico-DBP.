using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LabClinic.API.Controllers
{
    // Minimal stubs to allow building the small API project inside this workspace
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal sealed class ApiControllerAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    internal sealed class RouteAttribute : Attribute
    {
        public RouteAttribute(string template) { }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal sealed class HttpGetAttribute : Attribute
    {
        public HttpGetAttribute() { }
        public HttpGetAttribute(string template) { }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal sealed class HttpPostAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal sealed class HttpPutAttribute : Attribute
    {
        public HttpPutAttribute() { }
        public HttpPutAttribute(string template) { }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal sealed class HttpDeleteAttribute : Attribute
    {
        public HttpDeleteAttribute() { }
        public HttpDeleteAttribute(string template) { }
    }

    // Minimal action result and controller base stubs
    public interface IActionResult { }

    public abstract class ControllerBase
    {
        protected IActionResult Ok(object? value) => new OkResult(value);
        protected IActionResult NotFound() => new NotFoundResult();
        protected IActionResult BadRequest(string? message = null) => new BadRequestResult(message);
        protected IActionResult CreatedAtAction(string actionName, object? routeValues, object? value) => new CreatedAtActionResult(actionName, routeValues, value);
    }

    public sealed class OkResult : IActionResult { public object? Value { get; } public OkResult(object? value) { Value = value; } }
    public sealed class NotFoundResult : IActionResult { }
    public sealed class BadRequestResult : IActionResult { public string? Message { get; } public BadRequestResult(string? message) { Message = message; } }
    public sealed class CreatedAtActionResult : IActionResult { public string ActionName { get; } public object? RouteValues { get; } public object? Value { get; } public CreatedAtActionResult(string actionName, object? routeValues, object? value) { ActionName = actionName; RouteValues = routeValues; Value = value; } }

    // FromBody attribute stub
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class FromBodyAttribute : Attribute { }
}
