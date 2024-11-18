using System.Diagnostics.CodeAnalysis;

// Suppresses the warning CA1816: Dispose methods should call SuppressFinalize.
// This is because the assembly does not contain unmanaged resources that require finalization.
[assembly: SuppressMessage("Microsoft.Design", "CA1816:DisposeMethodsShouldCallSuppressFinalize", Justification = "No unmanaged resources in the assembly.")]

// Suppresses the warning CA2012: Dispose methods should call SuppressFinalize.
// This is because the assembly does not contain unmanaged resources that require finalization.
[assembly: SuppressMessage("Microsoft.Design", "CA2012:DisposeMethodsShouldCallSuppressFinalize", Justification = "No unmanaged resources in the assembly.")]
