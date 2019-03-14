Module DAORequetes
    'Fonctions du CRUD Utilisateur
    Public Const USR_CREATE = "PRAGMA foreign_keys = ON;INSERT INTO utilisateur values (null,?)"
    Public Const USR_READ = "SELECT * FROM utilisateur"
    Public Const USR_READBYID = "SELECT * FROM utilisateur where id_utilisateur = ? "
    Public Const USR_READBYNAME = "SELECT * FROM utilisateur where nom_utilisateur = ? "
    Public Const USR_UPDATE = "UPDATE utilisateur SET nom_utilisateur=? WHERE id_utilisateur=?"
    Public Const USR_DELETE = "DELETE FROM utilisateur WHERE id_utilisateur=?"
    Public Const USR_ERR_TYPEOF = "Utilisateur"

    'Fonctions du CRUD Signets
    Public Const SGT_CREATE = "INSERT INTO signets values (null,?,?,?,?)"
    Public Const SGT_READ = "SELECT * FROM signets"
    Public Const SGT_READBYID = "SELECT * FROM signets WHERE id_signet=?"
    Public Const SGT_UPDATE = "UPDATE signets SET clef_signet=?, valeur_signet=?, code_signet=?, id_impression=? WHERE id_signet=?"
    Public Const SGT_DELETE = "DELETE FROM signets WHERE id_signet=?"
    Public Const SGT_ERR_TYPEOF = "Signets"

    'Fonctions du CRUD Impression
    Public Const IMP_CREATE = "INSERT INTO impression values (null,?,?,?,?)"
    Public Const IMP_READ = "SELECT * FROM impression"
    Public Const IMP_READBYID = "SELECT * FROM impression WHERE id_impression=?"
    Public Const IMP_UPDATE = "UPDATE impression SET lot_impression=?, nomimprimante_impression=?, pages_impression=?, id_auditrails=? WHERE id_impression=?"
    Public Const IMP_DELETE = "DELETE FROM impression WHERE id_impression=?"
    Public Const IMP_ERR_TYPEOF = "Impression"

    'Fonctions du CRUD Auditrails
    Public Const AT_CREATE = "INSERT INTO auditrails values (null,?,?,?,?,?,?)"
    Public Const AT_READ = "SELECT * FROM auditrails"
    Public Const AT_READBYID = "SELECT * FROM auditrails WHERE id_auditrails=?"
    Public Const AT_UPDATE = "UPDATE auditrails SET nomfichier_auditrails=?, commentaire_auditrails=?, infosystem_auditrails=?, date_auditrails=?, id_utilisateur=? , id_operation=? WHERE id_auditrails=?"
    Public Const AT_DELETE = "DELETE FROM auditrails WHERE id_auditrails=?"
    Public Const AT_ERR_TYPEOF = "Auditrails"

    Public Const HVF_GETALL = "select * from histo_verification where id_auditrails = ?"
    Public Const HVF_GETLAST = "select * from histo_verification where id_auditrails = ? order by date_histo_verification DESC limit 1"
    Public Const HVF_INSERT = "INSERT into histo_verification values (?,?,?,?)"
    Public Const HVF_UPDATE = "UPDATE histo_verification set commentaire_histo_verification = ? where id_auditrails = ? and id_verification = ? and date_histo_verification = ?"
    Public Const HVF_ERR_TYPEOF = "HistoVérification"

    Public Const HDR_GETALL = "select * from histo_droits where id_utilisateur = ?"
    Public Const HDR_GETLAST = "select * from histo_droits where id_utilisateur = ? order by date_histo_droit DESC limit 1"
    Public Const HDR_INSERT = "INSERT into histo_droits values (?,?,?,?)"
    Public Const HDR_UPDATE = "UPDATE histo_droits set commentaire_histo_droit = ? where id_utilisateur = ? and id_droit = ? and date_histo_droit = ?"
    Public Const HDR_ERR_TYPEOF = "HistoDroit"

    Public Const ATP_ERR_TYPEOF = "AuditrailsImpression"

End Module
