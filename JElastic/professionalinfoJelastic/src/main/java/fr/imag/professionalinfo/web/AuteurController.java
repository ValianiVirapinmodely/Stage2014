package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Auteur;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/auteurs")
@Controller
@RooWebScaffold(path = "auteurs", formBackingObject = Auteur.class)
public class AuteurController {
}
