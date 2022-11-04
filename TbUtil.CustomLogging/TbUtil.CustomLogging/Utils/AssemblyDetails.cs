using System.Diagnostics;

namespace TbUtil.CustomLogging.Utils;

/// <summary>
/// Class to represent the assembly details for reflection
/// </summary>
public sealed record AssemblyDetails
{
    /// <summary>
    /// Property to keep the reflected file name
    /// </summary>
    public string GetFileName { get; set; }
    /// <summary>
    /// Property to keep the reflected line number
    /// </summary>
    public string GetLineNumber { get; set; }
    /// <summary>
    /// Property to keep the reflected calling method
    /// </summary>
    public string GetCaller { get; set; }

    /// <summary>
    /// Base constructor
    /// </summary>
    public AssemblyDetails()
    {
        GetFileName = string.Empty;
        GetLineNumber = string.Empty;
        GetCaller = string.Empty;
    }

}

/// <summary>
/// Methods to extract the reflection details
/// </summary>
public static class AssemblyDetailsActions
{

    private static int _frameIndex = 3;

    /// <summary>
    /// Sets the frame index based on how deep to reflect
    /// </summary>
    /// <param name="frameIndex"></param>
    public static void SetFrameIndex(int frameIndex)
    {
        _frameIndex = frameIndex;
    }

    /// <summary>
    /// Gets the assembly details using reflection
    /// </summary>
    /// <returns></returns>
    public static AssemblyDetails GetAssemblyDetails()
    {
        AssemblyDetails assemblyDetails = new();

        try
        {
            int frameIndex = _frameIndex;
            StackTrace stackTrace = new(true);

            frameIndex -= 1;
            frameIndex = frameIndex < 0 ? 1 : frameIndex;

            if (stackTrace.FrameCount > frameIndex)
            {
                StackFrame callerFrame = stackTrace.GetFrame(frameIndex);

                if (callerFrame is not null && callerFrame.HasMethod())
                {

                    assemblyDetails.GetFileName = callerFrame.GetFileName()?.ToString();
                    assemblyDetails.GetLineNumber = callerFrame.GetFileLineNumber().ToString();

                    System.Reflection.MethodBase method = callerFrame.GetMethod();
                    assemblyDetails.GetCaller = method is null
                        ? string.Empty
                        : $"{method.DeclaringType?.FullName}.{method.Name}({string.Join(", ", method.GetParameters()?.Select(pi => pi.ParameterType?.FullName))})";

                }
                else
                {
                    if (stackTrace.FrameCount > frameIndex - 1)
                    {

                        callerFrame = stackTrace.GetFrame(frameIndex - 1);

                        if (callerFrame is not null && callerFrame.HasMethod())
                        {
                            assemblyDetails.GetFileName = callerFrame.GetFileName()?.ToString();
                            assemblyDetails.GetLineNumber = callerFrame.GetFileLineNumber().ToString();

                            System.Reflection.MethodBase method = callerFrame.GetMethod();
                            assemblyDetails.GetCaller = method is null
                                ? string.Empty
                                : $"{method.DeclaringType?.FullName}.{method.Name}({string.Join(", ", method.GetParameters()?.Select(pi => pi.ParameterType?.FullName))})";
                        }
                        else
                        {
                            assemblyDetails.GetFileName = string.Empty;
                            assemblyDetails.GetLineNumber = string.Empty;
                            assemblyDetails.GetCaller = string.Empty;
                        }

                    }
                    else
                    {
                        assemblyDetails.GetFileName = string.Empty;
                        assemblyDetails.GetLineNumber = string.Empty;
                        assemblyDetails.GetCaller = string.Empty;
                    }
                }

            }
        }
        catch (Exception)
        {
            assemblyDetails = new AssemblyDetails();
        }

        return assemblyDetails;
    }

}
