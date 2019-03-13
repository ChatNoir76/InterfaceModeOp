''' <summary>
''' Interface pour les tables/bean commençant par histo_
''' </summary>
''' <typeparam name="Histo">nom de l'objet histo</typeparam>
''' <remarks></remarks>
Public Interface IDAO_Histo(Of Histo)

    Enum ObjetType As Byte
        Objet = 0
        Statut = 1
    End Enum

    ''' <summary>
    ''' Enregistre un nouvel objet de type Histo dans la base de données
    ''' </summary>
    ''' <param name="value">objet de type Histo à enregistrer</param>
    ''' <remarks></remarks>
    Sub dbInsert(ByRef value As Histo)

    ''' <summary>
    ''' récupère l'historique d'un objet
    ''' </summary>
    Function dbGetAllStatutById(ByRef id As Integer) As List(Of Histo)

    ''' <summary>
    ''' récupère le dernier statut d'un objet
    ''' </summary>
    Function dbGetLastStatutById(ByRef id As Integer) As Histo

    ''' <summary>
    ''' Mise à jour du commentaire de l'historique
    ''' </summary>
    ''' <param name="value">Objet de type Histo à mettre à jour</param>
    ''' <remarks></remarks>
    Sub dbUpdate(ByRef value As Histo)

    Function getUpdateParameters(ByVal value As Histo) As SQLite.SQLiteParameter()

    Function getInsertParameters(ByVal value As Histo) As SQLite.SQLiteParameter()

End Interface