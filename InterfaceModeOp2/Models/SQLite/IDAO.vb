Public Interface IDAO(Of T)

    Sub dbInsert(ByRef value As T)

    Function dbGetAll() As List(Of T)

    Function dbGetById(ByRef id As Integer) As T

    Function dbDelete(ByRef value As T) As Boolean

    Sub dbUpdate(ByRef value As T)

    Function getUpdateParameters(ByVal value As T) As SQLite.SQLiteParameter()

    Function getInsertParameters(ByVal value As T) As SQLite.SQLiteParameter()

End Interface
