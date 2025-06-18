using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Common.Constants
{
    public struct ApiErrorCodeMessages
    {

        #region Common error codes
        public const string FAILURE = "FAILURE";
        public const string SUCCESS = "SUCCESS";
        #endregion


        #region User error messages
        public const string UserNotFound = "User not found.";
        public const string UserAddedSuccessfully = "User added successfully.";
        #endregion

        #region
        public const string ProjectAddedSuccessfully = "Project added successfully.";
        public const string ProjectNotFound = "Project not found.";

        #endregion

        #region

        public const string TaskAddedSuccessfully = "Task added successfully.";
        public const string TaskUpdatedSuccessfully = "Task updated successfully.";
        public const string TaskNotFound = "Task not found.";
        public const string TaskDeletedSuccessfully = "Task deleted successfully.";
        #endregion
    }
}
