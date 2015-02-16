package fr.imag.professionalinfo.web;

import fr.imag.professionalinfo.domain.Langage;
import org.springframework.roo.addon.web.mvc.controller.scaffold.RooWebScaffold;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/langages")
@Controller
@RooWebScaffold(path = "langages", formBackingObject = Langage.class)
public class LangageController {
}
