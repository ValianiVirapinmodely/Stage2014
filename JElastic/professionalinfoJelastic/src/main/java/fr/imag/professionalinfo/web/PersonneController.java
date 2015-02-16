package fr.imag.professionalinfo.web;

import org.springframework.http.HttpStatus;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.ResponseStatus;

import fr.imag.professionalinfo.domain.Personne;
import fr.imag.professionalinfo.domain.PersonneSocial;
import fr.imag.professionalinfo.domain.Secteur;

@RequestMapping("/personnes")
@Controller
@RooWebScaffold(path = "personnes", formBackingObject = Personne.class)
public class PersonneController {

	@RequestMapping(value = "/createRest", method = RequestMethod.POST)
	@ResponseStatus(HttpStatus.CREATED)
	@ResponseBody
	public void createRest(@RequestBody PersonneSocial personneSocial) {
		Personne personne;
		
		personne = new Personne();
		personne.setNom(personneSocial.getNom());
		personne.setPrenom(personneSocial.getPrenom());
		personne.setURLPhoto(personneSocial.getUrlPhoto());
		
		personne.setId(0L);
		personne.setVersion(0);
		
		System.out.println(personne);
		personne.persist();
	}
}
