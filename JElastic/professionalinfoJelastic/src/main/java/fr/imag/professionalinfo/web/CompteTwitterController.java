package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.CompteTwitter;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/comptetwitters")
@Controller
@RooWebScaffold(path = "comptetwitters", formBackingObject = CompteTwitter.class)
public class CompteTwitterController {
}
