package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Entreprise;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/entreprises")
@Controller
@RooWebScaffold(path = "entreprises", formBackingObject = Entreprise.class)
public class EntrepriseController {
}
