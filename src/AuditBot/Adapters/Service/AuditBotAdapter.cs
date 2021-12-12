using AuditBot.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AuditBot.Adapters.Service
{
    /// <summary>
    /// Adapter for the action filters, only to be used for low-level customization
    /// </summary>
    public class AuditBotAdapter
    {
        /// <summary>
        /// Determines if a Web API action is ignored by attributes, and if it is ignored, discards the current audit scope.
        /// </summary>
        public bool ActionIgnored(ActionExecutingContext actionContext)
        {
            bool ignored = false;
            var actionDescriptor = actionContext?.ActionDescriptor as ControllerActionDescriptor;
            var controllerIgnored = actionDescriptor?.MethodInfo?.DeclaringType.GetTypeInfo().GetCustomAttribute<IgnoreAuditorAttribute>(true);
            if (controllerIgnored != null)
            {
                ignored = true;
            }
            else
            {
                var actionIgnored = actionDescriptor?.MethodInfo?.GetCustomAttribute<IgnoreAuditorAttribute>(true);
                if (actionIgnored != null)
                {
                    ignored = true;
                }
            }
            if (ignored)
            {
                //DiscardCurrentScope(actionContext.HttpContext);
            }
            return ignored;
        }

        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        internal async Task BeforeExecutingAsync(ActionExecutingContext actionContext,
            bool includeHeaders, bool includeRequestBody, bool serializeParams, string eventTypeName)
        {
            var httpContext = actionContext.HttpContext;
            var actionDescriptor = actionContext.ActionDescriptor as ControllerActionDescriptor;

            //var auditAction = await CreateOrUpdateAction(actionContext, includeHeaders, includeRequestBody, serializeParams, eventTypeName);

            //var eventType = (eventTypeName ?? "{verb} {controller}/{action}").Replace("{verb}", auditAction.HttpMethod)
            //    .Replace("{controller}", auditAction.ControllerName)
            //    .Replace("{action}", auditAction.ActionName)
            //    .Replace("{url}", auditAction.RequestUrl);

            //// Create the audit scope
            //var auditEventAction = new AuditEventWebApi()
            //{
            //    Action = auditAction
            //};
            //var auditScope = await AuditScope.CreateAsync(new AuditScopeOptions() { EventType = eventType, AuditEvent = auditEventAction, CallingMethod = actionDescriptor.MethodInfo });
            //httpContext.Items[AuditApiHelper.AuditApiActionKey] = auditAction;
            //httpContext.Items[AuditApiHelper.AuditApiScopeKey] = auditScope;

            await Task.CompletedTask;
        }


        /// <summary>
        /// Occurs after the action method is invoked.
        /// </summary>
        internal async Task AfterExecutedAsync(ActionExecutedContext context, bool includeModelState, bool includeResponseBody, bool includeResponseHeaders)
        {
            //var httpContext = context.HttpContext;
            //var auditAction = httpContext.Items[AuditApiHelper.AuditApiActionKey] as AuditApiAction;
            //var auditScope = httpContext.Items[AuditApiHelper.AuditApiScopeKey] as AuditScope;
            //if (auditAction != null && auditScope != null)
            //{
            //    auditAction.Exception = context.Exception.GetExceptionInfo();
            //    auditAction.ModelStateErrors = includeModelState ? AuditApiHelper.GetModelStateErrors(context.ModelState) : null;
            //    auditAction.ModelStateValid = includeModelState ? context.ModelState?.IsValid : null;
            //    if (context.HttpContext.Response != null && context.Result != null)
            //    {
            //        var statusCode = context.Result is ObjectResult && (context.Result as ObjectResult).StatusCode.HasValue ? (context.Result as ObjectResult).StatusCode.Value
            //            : context.Result is StatusCodeResult ? (context.Result as StatusCodeResult).StatusCode : context.HttpContext.Response.StatusCode;
            //        auditAction.ResponseStatusCode = statusCode;
            //        auditAction.ResponseStatus = AuditApiHelper.GetStatusCodeString(auditAction.ResponseStatusCode);
            //        if (includeResponseBody)
            //        {
            //            var bodyType = context.Result.GetType().GetFullTypeName();
            //            auditAction.ResponseBody = new BodyContent { Type = bodyType, Value = GetResponseBody(context.ActionDescriptor, context.Result) };
            //        }

            //        if (includeResponseHeaders)
            //        {
            //            auditAction.ResponseHeaders = AuditApiHelper.ToDictionary(httpContext.Response.Headers);
            //        }
            //    }
            //    else
            //    {
            //        auditAction.ResponseStatusCode = 500;
            //        auditAction.ResponseStatus = "Internal Server Error";
            //    }

            //    // Replace the Action field 
            //    (auditScope.Event as AuditEventWebApi).Action = auditAction;
            //    // Save, if action was not created by middleware
            //    if (!auditAction.IsMiddleware)
            //    {
            //        await auditScope.DisposeAsync();
            //    }
            //}

            await Task.CompletedTask;
        }

        private object GetResponseBody(ActionDescriptor descriptor, IActionResult result)
        {
            if ((descriptor as ControllerActionDescriptor)?.MethodInfo
                .ReturnTypeCustomAttributes
                .GetCustomAttributes(typeof(IgnoreAuditorAttribute), true)
                .Any() == true)
            {
                return null;
            }
            if (result is ObjectResult or)
            {
                return or.Value;
            }
            if (result is StatusCodeResult sr)
            {
                return sr.StatusCode;
            }
            //if (result is JsonResult jr)
            //{
            //    return jr.Value;
            //}
            if (result is ContentResult cr)
            {
                return cr.Content;
            }
            if (result is FileResult fr)
            {
                return fr.FileDownloadName;
            }
            if (result is LocalRedirectResult lrr)
            {
                return lrr.Url;
            }
            if (result is RedirectResult rr)
            {
                return rr.Url;
            }
            if (result is RedirectToActionResult rta)
            {
                return rta.ActionName;
            }
            if (result is RedirectToRouteResult rtr)
            {
                return rtr.RouteName;
            }
            if (result is SignInResult sir)
            {
                return sir.Principal?.Identity?.Name;
            }
            if (result is RedirectToPageResult rtp)
            {
                return rtp.PageName;
            }
            return result.ToString();
        }

        private IDictionary<string, object> GetActionParameters(ControllerActionDescriptor actionDescriptor, IDictionary<string, object> actionArguments, bool serializeParams)
        {
            var args = actionArguments.ToDictionary(k => k.Key, v => v.Value);
            foreach (var param in actionDescriptor.Parameters)
            {
                var paramDescriptor = param as ControllerParameterDescriptor;
                if (paramDescriptor?.ParameterInfo.GetCustomAttribute<IgnoreAuditorAttribute>(true) != null)
                {
                    args.Remove(param.Name);
                }
                else if (paramDescriptor?.ParameterInfo.GetCustomAttribute<FromServicesAttribute>(true) != null)
                {
                    args.Remove(param.Name);
                }
            }
            //if (serializeParams)
            //{
            //    return AuditApiHelper.SerializeParameters(args);
            //}
            return args;
        }
    }
}
