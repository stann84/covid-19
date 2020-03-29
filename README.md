# Xamarin-Firebase
Test pour l'utilisation des différents services de Firebase avec Xamarin Forms. 
l'Authentification pour s'identifier et créer un nouveau compte utilisateur.
le Storage pour stocker une photo de profil par utilisateur.
Le Database pour enregistrer les informations des utilisateurs.
et Le service de Cloud Messaging, pour envoyer et recevoir des notifications.

- Il faudra ajouter à la racine du projet .droid votre fichier google-services.json créé par firebase lors de la création de votre application android. sans oublier de mettre dans VS la propriété action de génération de ce fichier avec la valeur GoogleServicesJson.
- Dans le fichier EditPage mettre l'adresse de votre application Firebase pour le Storage & la Database.
- Dans Android manifeste, mettre le nom de package identique à celui que vous avez entré dans la console firebase lors de sa création.
- Ne pas oublier également dans le manifeste d'indiquer ce nom de package dans le provider pour pouvoir uploader des fichiers avec le plugin.media
- Enfin, pour les notifications, ajoutez les paramètres du projet Cloud messaging; copiez la Clé de serveur et coller là dans la variable applicationID du fichier NotificationHelper.cs. Idem pour l'ID de l'expéditeur, copiez la et coller là dans la variable senderId du même fichier.
