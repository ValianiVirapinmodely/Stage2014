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
public class Brevet {

    @Temporal(TemporalType.TIMESTAMP)
    @DateTimeFormat(style = "M-")
    private Date DateAttribution;

    @Size(min = 0, max = 0)
    private String Titre;

    @Size(min = 0, max = 0)
    private String Numero;

    private String Id2;

    @Size(min = 0, max = 0)
    private String URLBrevet;

    private String IntituleStatut;

    private String Statut;

    @Size(min = 0, max = 0)
    private String Resume;

    @ManyToOne
    private Organisme BrevetOrganisme;

    @ManyToOne
    private Inventeur BrevetInventeur;
}
