package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Secteur;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/secteurs")
@Controller
@RooWebScaffold(path = "secteurs", formBackingObject = Secteur.class)
public class SecteurController {
}
