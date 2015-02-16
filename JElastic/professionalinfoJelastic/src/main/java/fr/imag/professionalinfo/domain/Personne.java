package fr.imag.professionalinfo.domain;

import java.util.Date;

import javax.persistence.ManyToOne;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;
import javax.validation.constraints.Size;

import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.roo.addon.javabean.RooJavaBean;
import org.springframework.roo.addon.jpa.activerecord.RooJpaActiveRecord;
import org.springframework.roo.addon.tostring.RooToString;

@RooJavaBean
@RooToString
@RooJpaActiveRecord
public class Personne {

    private String RecompensesDonnees;

    @Size(min = 0, max = 0)
    private String Lieu;

    private String Property1;

    private String NombreTotalPositionsCourantes;

    @Size(min = 0, max = 0)
    private String URLComplete;

    private String NombreConnexionsPlafonne;

    @Size(min = 0, max = 0)
    private String NomDeJeuneFille;

    @Size(min = 0, max = 0)
    private String CodeLieu;

    private String TitreAPIValeur;

    @Size(min = 0, max = 0)
    private String ReactionAuxOffres;

    @Size(min = 0, max = 0)
    private String URLAPI;

    private String EntitesSuivies;

    @Size(min = 0, max = 0)
    private String URLLinkedInAuth;

    @Size(min = 0, max = 0)
    private String Adresse;

    private String Property2;

    private String Id2;

    @Size(min = 0, max = 0)
    private String Reseau;

    @Size(min = 0, max = 0)
    private String Titre;

    @Size(min = 0, max = 0)
    private String RelationVisiteurDistance;

    @Size(min = 0, max = 0)
    private String Prenom;

    private String NombreTotalConnexions;

    private String NombreTotalEtablissementsFormation;

    private String NombreTotalPositions;

    @Size(min = 0, max = 0)
    private String Associations;

    @Size(min = 0, max = 0)
    private String NomFormate;

    private String TitreAPINom;

    @Size(min = 0, max = 0)
    private String PrenomPhonetique;

    @Size(min = 0, max = 0)
    private String NomPhonetiqueFormate;

    private String Property3;

    private String Groupes;

    @Size(min = 0, max = 0)
    private String RelationVisiteurConnexion;

    @Temporal(TemporalType.TIMESTAMP)
    @DateTimeFormat(style = "M-")
    private Date DateDeNaissance;

    private String NombreRecommandations;

    private String NombreTotalPositionsPassees;

    private String TempsDerniereModifProfil;

    @Size(min = 0, max = 0)
    private String PartageCourant;

    @Size(min = 0, max = 0)
    private String URLNom;

    @Size(min = 0, max = 0)
    private String Nom;

    private String Distance;

    @Size(min = 0, max = 0)
    private String RelationVisiteurLien;

    @Size(min = 0, max = 0)
    private String URLLinkedInPublic;

    @Size(min = 0, max = 0)
    private String Etablissement;

    private String Connexions;

    private String NombreTotalExperiencesVolontariat;

    private String Property4;

    private String JobsSuivis;

    private String SuggestionsSuivis;

    private String URLPartagees;

    @Size(min = 0, max = 0)
    private String URLPhoto;

    @Size(min = 0, max = 0)
    private String Email;

    @Size(min = 0, max = 0)
    private String NomPhonetique;

    private String NombreTotalCours;

    private String NombreConnexions;

    @Size(min = 0, max = 0)
    private String Interets;

    @ManyToOne
    private ExperienceVolontaire ExperiencePersonne;

    @ManyToOne
    private Position_ PositionEntreprise;

    @ManyToOne
    private fr.imag.professionalinfo.domain.Personne AutresProfilsVisites;

    @ManyToOne
    private Cours NiveauCours;

    @ManyToOne
    private Telephone TelephoneLie;

    @ManyToOne
    private Publication PublicationPersonne;

    @ManyToOne
    private Competence CompetencePersonne;

    @ManyToOne
    private Certification NiveauCertification;

    @ManyToOne
    private CompteTwitter CompteTwitterAssocie;

    @ManyToOne
    private MessagerieInstantaneeLiee MessageriePersonnelle;

    @ManyToOne
    private Langage LanguesParlees;

    @ManyToOne
    private Brevet BrevetsObtenus;

    @ManyToOne
    private Education NiveauEducation;
}
