package fr.imag.professionalinfo.domain;

import javax.persistence.ManyToOne;
import javax.validation.constraints.Size;
import org.springframework.roo.addon.javabean.RooJavaBean;
import org.springframework.roo.addon.jpa.activerecord.RooJpaActiveRecord;
import org.springframework.roo.addon.tostring.RooToString;

@RooJavaBean
@RooToString
@RooJpaActiveRecord
public class CompteLie {

    @Size(min = 0, max = 0)
    private String Type;

    private String Property1;

    private String EstPrimaire;

    @Size(min = 0, max = 0)
    private String BindingStatus;

    @ManyToOne
    private Personne Membre;

    @ManyToOne
    private FournisseurDuCompte CompteFournisseur;
}
