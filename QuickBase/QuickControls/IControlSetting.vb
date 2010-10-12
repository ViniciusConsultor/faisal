'Author: Faisal Saleem
'Date Created(DD-MMM-YY): 14-Feb-2010
'***** Modification History *****
'                 Date      Description
'Name          (DD-MMM-YY) 
'--------------------------------------------------------------------------------
'
''' <summary>
''' This interface will get setting from database for this control.
''' </summary>
Public Interface IControlSetting

#Region "Declarations"

#End Region

#Region "Properties"

  Property IsReadonlyForNewRecord() As Boolean
  Property IsReadonlyForExistingRecord() As Boolean
  Property IsMandatory() As Boolean

#End Region

#Region "Methods"

#End Region

#Region "Event Methods"

#End Region

End Interface
