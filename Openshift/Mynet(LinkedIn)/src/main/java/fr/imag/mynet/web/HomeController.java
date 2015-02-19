package fr.imag.mynet.web;

import java.security.Principal;
import java.util.Arrays;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import javax.inject.Inject;

import org.springframework.social.connect.Connection;
import org.springframework.social.connect.ConnectionRepository;
import org.springframework.social.linkedin.api.Company;
import org.springframework.social.linkedin.api.LinkedIn;
import org.springframework.social.linkedin.api.LinkedInProfile;
import org.springframework.social.linkedin.api.LinkedInProfileFull;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import fr.imag.mynet.domain.Entreprise;
import fr.imag.mynet.domain.Personne;

/**
 * 
 * @author jccastrejon
 * 
 */
@Controller
public class HomeController {

	

	@Inject
	private ConnectionRepository connectionRepository;


	@RequestMapping(value = "/", method = RequestMethod.GET)
	public String home(Principal currentUser, Model model) {
		Connection<LinkedIn> connection;
		LinkedInProfileFull profile;
		Personne personne;
		List<Company> companies;
		
		// Get the object from linkedin
		connection = connectionRepository.findPrimaryConnection(LinkedIn.class);
		profile = connection.getApi().profileOperations().getUserProfileFull();
		companies = connection.getApi().companyOperations().getFollowing();
		
		// Get the data that you need from the linkedin answer
		personne = new Personne();
		personne.setNom(profile.getLastName());
		personne.setPrenom(profile.getFirstName());
		personne.setUrlPhoto(profile.getProfilePictureUrl());
		
		// Updates (25/01/2015)
		/******/
		personne.setIdentifiant(profile.getId());
		personne.setEmail(profile.getEmailAddress());
		personne.setTitre(profile.getHeadline());
		personne.setIndustrie(profile.getIndustry());
		personne.setResume(profile.getSummary());
		personne.setUrlPublique(profile.getPublicProfileUrl());
		personne.setStandardURL(profile.getSiteStandardProfileRequest());
		
		Map<String, Object> extra = profile.getExtraData();
		
		if (extra.containsKey("languages")) {
			personne.setExtraData(extra);
		}
		
		personne.setSkills(profile.getSkills());
		personne.setConnectionNumber(profile.getNumConnections());
		personne.setPhones(profile.getPhoneNumbers());
		personne.setEducation(profile.getEducations());
		personne.setPositions(profile.getPositions());
		
		
		//Companies handling
		int i= 0;
		for (Company company : companies) {
			
			Entreprise entreprise= new Entreprise();
			entreprise.setDescription(company.getDescription());
			entreprise.setName(company.getName());
			entreprise.setFoundedYear(company.getFoundedYear());
			entreprise.setEmployeeCountRange(company.getEmployeeCountRange()); 
			entreprise.setIndustry(company.getIndustry());
			model.addAttribute("entreprise" + i , entreprise);
			i++;
		}
		/******/
		
		// Save the data in your other application by invoking the REST interface
		// TODO: Invocation
		// Example:
		// RestTemplate restTemplate = new RestTemplate();
		// restTemplate.postForObject(myNetData.getMyNetContactsUri() + "/groups/createRest", group, Object.class);
		
		// This is just to test the data that we retrieved...
		model.addAttribute("personne", personne);
		
		return "linkedinProfile";
	}
}