package fr.imag.professionalinfo.domain;

import javax.persistence.ManyToOne;
import org.springframework.roo.addon.javabean.RooJavaBean;
import org.springframework.roo.addon.jpa.activerecord.RooJpaActiveRecord;
import org.springframework.roo.addon.tostring.RooToString;

@RooJavaBean
@RooToString
@RooJpaActiveRecord
public class CompteTwitter {

    private String Id2;

    private String EstPrimaire;

    @ManyToOne
    private FournisseurDuCompte TwitterFournisseur;
}
