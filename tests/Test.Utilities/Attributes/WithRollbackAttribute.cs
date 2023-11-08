using System.Reflection;
using System.Transactions;
using Xunit.Sdk;

namespace Test.Utilities.Attributes
{
    public class WithRollbackAttribute : BeforeAfterTestAttribute
    {
        private TransactionScope _scope;

        public override void Before(MethodInfo methodUnderTest)
        {
            _scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.MaximumTimeout,
                },
                TransactionScopeAsyncFlowOption.Enabled
            );
        }

        public override void After(MethodInfo methodUnderTest)
        {
            _scope.Dispose();
        }
    }
}
