import Vue from 'vue';
import VueI18n from 'vue-i18n';

Vue.use(VueI18n);

const messages = {
    'fr': {
        'fr': 'Fr',
        'en': 'En',
        'frLong': 'Français',
        'enLong': 'Anglais',
        'languageChangeFor': 'Langue changée pour',

        //Header
        'apiDocumentation': 'Documentation',
        'apiVersion': "Version de l'API",
        'rawApi': "API",
        'resetData': "Réinitialiser données",

        'predefinedQueries': 'Requêtes prédéfinies:',
        'additionalParameters': 'Paramètres additionnels :',
        'whatAreYouSearchingFor': 'Que cherchez-vous ?',
        'maxResults': 'Résultats :',
        'refreshData': 'Rafraîchir',

        //Suggestion table
        'locationName': "Nom de l'endroit",
        'latitude': 'Latitude',
        'longitude': 'Longitude',
        'score': 'Score',
        'details': 'Détails',
        'noResults': "Il n'y a aucun résultat",

        'dataWasReset': 'Les données ont été réinitialisées!',
        'apiVersionChanged': "La version de l'API a été remplacée par"
    },

    'en': {
        'fr': 'Fr',
        'en': 'En',
        'frLong': 'French',
        'enLong': 'English',
        'languageChangeFor': 'Language changed for',

        //Header
        'apiDocumentation': 'Documentation',
        'apiVersion': "API version",
        'rawApi': "API",
        'resetData': "Reset data",

        'predefinedQueries': 'Predefined queries:',
        'additionalParameters': 'Additional parameters :',
        'whatAreYouSearchingFor': 'What are you searching for ?',
        'maxResults': 'Results :',
        'refreshData': 'Refresh data',

        //Suggestion table
        'locationName': 'Location name',
        'latitude': 'Latitude',
        'longitude': 'Longitude',
        'score': 'Score',
        'details': 'Details',
        'noResults': 'There are no results to show',

        'dataWasReset': 'Data was reset!',
        'apiVersionChanged': 'API version was changed to '
    }
};

const i18n = new VueI18n({
    locale: 'fr', // set locale
    fallbackLocale: 'en', // set fallback locale
    messages, // set locale messages
});

export default i18n;