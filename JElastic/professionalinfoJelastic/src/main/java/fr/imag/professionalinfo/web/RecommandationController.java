package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Recommandation;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/recommandations")
@Controller
@RooWebScaffold(path = "recommandations", formBackingObject = Recommandation.class)
public class RecommandationController {
}
