namespace ClaimDigitalize.Models
{
    public struct ValidationMessages
    {
        public const string Success = "Your desired operation has been completed ";
        public const string AlreadyExists = "Record you are trying to save is already exists,please try different to continue.";
        public const string Error = "Oops! Some thing went wrong";
        public const string Deleted = "Record deleted successfully";
        public const string ValidationError = "Be nice to your action! Please fill required fields";
        public const string ForeignKeyViolation = "Can't delete record because there is at least one relationship with some other ";
        public const string AlreadyDeleted = "Unable to save changes. The record was deleted by another user.";

        public const string HasModified = "The record you attempted to edit was modified by another user after you got the original value.</ br> The edit operation was canceled and the current values in the database have been displayed. If you still want to edit this record, reload the page again.";

        public const string NotFound = " The record you are trying to access is not found,please try different to continue.";

        public const string InvalidUser = "Invalid username or password";
    }
}