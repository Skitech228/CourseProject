#region Using derectives

using System;

#endregion

namespace CourseProject.Application.Extentions
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}