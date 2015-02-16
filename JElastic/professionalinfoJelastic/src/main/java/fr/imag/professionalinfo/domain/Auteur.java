package fr.imag.professionalinfo.domain;

import javax.persistence.ManyToOne;
import javax.validation.constraints.Size;
import org.springframework.roo.addon.javabean.RooJavaBean;
import org.springframework.roo.addon.jpa.activerecord.RooJpaActiveRecord;
import org.springframework.roo.addon.tostring.RooToString;

@RooJavaBean
@RooToString
@RooJpaActiveRecord
public class Auteur {

    @Size(min = 0, max = 0)
    private String Nom;

    private String Id2;

    @ManyToOne
    private Personne AuteurPersonne;
}
