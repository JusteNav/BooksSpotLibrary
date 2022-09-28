using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace BooksSpotLibrary.Constants
{
    public class OperationNames
    {
            public static readonly string CreateOperationName = "Create";
            public static readonly string ReadOperationName = "Read";
            public static readonly string UpdateOperationName = "Update";
            public static readonly string DeleteOperationName = "Delete";
            public static readonly string ViewDetailsOperationName = "Details";
            public static readonly string ReturnOperationName = "Return";
            public static readonly string ReserveOperationName = "Reserve";
            public static readonly string BorrowOperationName = "Borrow";
            public static readonly string CancelReservationOperationName = "Cancel Reservation";

        public static OperationAuthorizationRequirement Create =
          new OperationAuthorizationRequirement { Name = CreateOperationName };
        public static OperationAuthorizationRequirement Read =
          new OperationAuthorizationRequirement { Name = ReadOperationName };
        public static OperationAuthorizationRequirement Update =
          new OperationAuthorizationRequirement { Name = UpdateOperationName };
        public static OperationAuthorizationRequirement Delete =
          new OperationAuthorizationRequirement { Name = DeleteOperationName };
        public static OperationAuthorizationRequirement Details =
          new OperationAuthorizationRequirement { Name = ViewDetailsOperationName };
        public static OperationAuthorizationRequirement Return =
          new OperationAuthorizationRequirement { Name = ReturnOperationName };
        public static OperationAuthorizationRequirement Reserve =
          new OperationAuthorizationRequirement { Name = ReserveOperationName };
        public static OperationAuthorizationRequirement Borrow =
          new OperationAuthorizationRequirement { Name = BorrowOperationName };
        public static OperationAuthorizationRequirement CancelReservation =
          new OperationAuthorizationRequirement { Name = CancelReservationOperationName };
    }
}
