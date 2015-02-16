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
public class Publication {

    @Temporal(TemporalType.TIMESTAMP)
    @DateTimeFormat(style = "M-")
    private Date DatePublication;

    @Size(min = 0, max = 0)
    private String Titre;

    @Size(min = 0, max = 0)
    private String Resume;

    @Size(min = 0, max = 0)
    private String Editeur;

    private String Id2;

    @Size(min = 0, max = 0)
    private String URL;

    @ManyToOne
    private Auteur PublicationDeAuteur;
}
