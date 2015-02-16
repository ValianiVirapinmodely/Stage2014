package fr.imag.professionalinfo.domain;

import javax.persistence.ManyToOne;
import javax.validation.constraints.Size;
import org.springframework.roo.addon.javabean.RooJavaBean;
import org.springframework.roo.addon.jpa.activerecord.RooJpaActiveRecord;
import org.springframework.roo.addon.tostring.RooToString;

@RooJavaBean
@RooToString
@RooJpaActiveRecord
public class Entreprise {

    @Size(min = 0, max = 0)
    private String Type;

    @Size(min = 0, max = 0)
    private String Secteur;

    @Size(min = 0, max = 0)
    private String NomBourse;

    private String Id2;

    private String NombreEmployes;

    @Size(min = 0, max = 0)
    private String NomOfficiel;

    @ManyToOne
    private Secteur SecteurEntreprise;
}
