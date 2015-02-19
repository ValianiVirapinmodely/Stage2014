package fr.imag.mynet.domain;

import java.util.Arrays;
import java.util.List;
import java.util.Map;

import org.springframework.social.linkedin.api.Education;
import org.springframework.social.linkedin.api.PhoneNumber;
import org.springframework.social.linkedin.api.Position;
import org.springframework.social.linkedin.api.UrlResource;

public class Personne {
	private String nom;
	private String prenom;
	private String urlPhoto;
	private String identifiant;
	private String email;
	private String titre;
	private String stringSkills;
	private int connectionNumber;
	private List<PhoneNumber> phones;
	private List<String> skills;
	private String industrie;
	private String resume;
	private String urlPublique;
	private String standardURLName;
	private String standardURLBrowser;
	
	private Map<String, Object> extraData;
	private String extraDataContenu;
	
	private List<Education> education;
	private String positions;
	private int numberOfPos;

	public String getNom() {
		return nom;
	}

	public void setNom(String nom) {
		this.nom = nom;
	}

	public String getPrenom() {
		return prenom;
	}

	public void setPrenom(String prenom) {
		this.prenom = prenom;
	}

	public String getUrlPhoto() {
		return urlPhoto;
	}

	public void setUrlPhoto(String urlPhoto) {
		this.urlPhoto = urlPhoto;
	}

	public String getIdentifiant() {
		return identifiant;
	}

	public void setIdentifiant(String identifiant) {
		this.identifiant = identifiant;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getTitre() {
		return titre;
	}

	public void setTitre(String titre) {
		this.titre = titre;
	}

	public String getIndustrie() {
		return industrie;
	}

	public void setIndustrie(String industrie) {
		this.industrie = industrie;
	}

	public String getResume() {
		return resume;
	}

	public void setResume(String resume) {
		this.resume = resume;
	}

	public String getUrlPublique() {
		return urlPublique;
	}

	public void setUrlPublique(String urlPublique) {
		this.urlPublique = urlPublique;
	}

	public String getStandardURLName() {
		return standardURLName;
	}
	
	public String getStandardURLBrowser() {
		return standardURLBrowser;
	}

	public void setStandardURL(UrlResource standardURL) {
		this.standardURLName = standardURL.getName();
		this.standardURLBrowser = standardURL.getUrl();
	}

	public Map<String, Object> getExtraData() {
		return extraData;
	}

	public void setExtraData(Map<String, Object> extraData) {
		this.extraData = extraData;
		this.extraDataContenu = Arrays.toString(extraData.entrySet().toArray());
	}
	
	public String getExtraDataContenu() {
		return extraDataContenu;
	}

	public List<String> getSkills() {
		return skills;
	}

	public void setSkills(List<String> skills) {
		this.skills = skills;
		this.setStringSkills(skills.toString());
	}

	public String getStringSkills() {
		return stringSkills;
	}

	public void setStringSkills(String stringSkills) {
		this.stringSkills = stringSkills;
	}

	public int getConnectionNumber() {
		return connectionNumber;
	}

	public void setConnectionNumber(int connectionNumber) {
		this.connectionNumber = connectionNumber;
	}

	public List<PhoneNumber> getPhones() {
		return phones;
	}

	public void setPhones(List<PhoneNumber> phones) {
		this.phones = phones;
	}

	public List<Education> getEducation() {
		return education;
	}

	public void setEducation(List<Education> education) {
		this.education = education;
	}
	
	public String getPositions() {
		return positions;
	}

	public void setPositions(List<Position> positions) {
		this.numberOfPos = positions.size();
		for (int i=0; i<positions.size(); i++) {
			this.positions += "--"+positions.get(i).getTitle();
		}
	}
	
	public int getNumberOfPos() {
		return numberOfPos;
	}
	
}
