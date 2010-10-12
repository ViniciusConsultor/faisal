Imports System
Imports System.Text
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.VisualStudio.TeamSystem.Data.UnitTesting
Imports Microsoft.VisualStudio.TeamSystem.Data.UnitTesting.Conditions

<TestClass()> _
Public Class ReportCriteria
    Inherits DatabaseTestClass

    Sub New()
        InitializeComponent()
    End Sub

    <TestInitialize()> _
    Public Sub TestInitialize()
        InitializeTest()
    End Sub

    <TestCleanup()> _
    Public Sub TestCleanup()
        CleanupTest()
    End Sub

    <TestMethod()> _
    Public Sub DatabaseTest1()
        Dim testActions As DatabaseTestActions = Me.DatabaseTest1Data
        'Execute the pre-test script
        '
        System.Diagnostics.Trace.WriteLineIf((Not (testActions.PretestAction) Is Nothing), "Executing pre-test script...")
        Dim pretestResults() As ExecutionResult = TestService.Execute(Me.PrivilegedContext, Me.PrivilegedContext, testActions.PretestAction)
        'Execute the test script
        '
        System.Diagnostics.Trace.WriteLineIf((Not (testActions.TestAction) Is Nothing), "Executing test script...")
        Dim testResults() As ExecutionResult = TestService.Execute(Me.ExecutionContext, Me.PrivilegedContext, testActions.TestAction)
        'Execute the post-test script
        '
        System.Diagnostics.Trace.WriteLineIf((Not (testActions.PosttestAction) Is Nothing), "Executing post-test script...")
        Dim posttestResults() As ExecutionResult = TestService.Execute(Me.PrivilegedContext, Me.PrivilegedContext, testActions.PosttestAction)
    End Sub

#Region "Designer support code"

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()

        Me.DatabaseTest1Data = New Microsoft.VisualStudio.TeamSystem.Data.UnitTesting.DatabaseTestActions
        '
        'DatabaseTest1Data
        '
        Me.DatabaseTest1Data.PosttestAction = Nothing
        Me.DatabaseTest1Data.PretestAction = Nothing
        Me.DatabaseTest1Data.TestAction = Nothing

    End Sub

#End Region

#Region "Additional test attributes"
    '
    ' You can use the following additional attributes as you write your tests:
    '
    ' Use ClassInitialize to run code before running the first test in the class
    ' <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    ' End Sub
    '
    ' Use ClassCleanup to run code after all tests in a class have run
    ' <ClassCleanup()> Public Shared Sub MyClassCleanup()
    ' End Sub
    '
#End Region

    Private DatabaseTest1Data As DatabaseTestActions
End Class

