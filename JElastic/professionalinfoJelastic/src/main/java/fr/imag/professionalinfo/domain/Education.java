package fr.imag.professionalinfo.domain;

import java.util.Date;
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
public class Education {

    @Size(min = 0, max = 0)
    private String AutresDetails;

    @Temporal(TemporalType.TIMESTAMP)
    @DateTimeFormat(style = "M-")
    private Date DateDebut;

    @Size(min = 0, max = 0)
    private String Diplome;

    @Size(min = 0, max = 0)
    private String Secteur;

    @Size(min = 0, max = 0)
    private String NomEcole;

    private String Id2;

    @Temporal(TemporalType.TIMESTAMP)
    @DateTimeFormat(style = "M-")
    private Date DateFin;

    @Size(min = 0, max = 0)
    private String Activites;
}
