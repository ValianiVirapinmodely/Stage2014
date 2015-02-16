package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Inventeur;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/inventeurs")
@Controller
@RooWebScaffold(path = "inventeurs", formBackingObject = Inventeur.class)
public class InventeurController {
}
