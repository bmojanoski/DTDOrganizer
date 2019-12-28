using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace DTDOrganizer.Patterns
{
    public class GoogleBooksApiReader : TextReader
    {
        private JObject bookData;

        public GoogleBooksApiReader(HttpWebResponse apiCallResponse)
        {
            bookData = deserializeJsonResponse(apiCallResponse.GetResponseStream());
        }

        private JObject deserializeJsonResponse(Stream responseStream)
        {
            return JObject.Parse(new StreamReader(responseStream).ReadToEnd());
        }

        public string getKind()
        {
            var kind = (string)bookData["kind"];
            return kind ?? "";
        }

        public bool isValid()
        {
            return int.Parse((string)bookData["totalItems"]) != 0 ? true : false;
        }

        public string getId()
        {
            var id = (string)bookData["items"][0]["id"];
            return id ?? "";
        }

        public string getEtag()
        {
            var etag = (string)bookData["items"][0]["etag"];
            return etag ?? "";
        }

        public string getSelfLink()
        {
            var selfLink = (string)bookData["items"][0]["selfLink"];
            return selfLink ?? "";
        }

        public string getTitle()
        {
            var title = (string)bookData["items"][0]["volumeInfo"]["title"];
            return title ?? "";
        }

        public string getSubtitle()
        {
            var subtitle = (string)bookData["items"][0]["volumeInfo"]["subtitle"];
            return subtitle ?? "";
        }

        public List<string> getAuthors()
        {
            JArray authors = ((JArray)bookData["items"][0]["volumeInfo"]["authors"]);
            return authors.Any() ? authors.Select(a => (string)a).ToList() : new List<string>();
        }

        public string getPublisher()
        {
            var publisher = (string)bookData["items"][0]["volumeInfo"]["publisher"];
            return publisher ?? "";
        }

        public string getPublishingDate()
        {
            var publishingDate = (string)bookData["items"][0]["volumeInfo"]["publishedDate"];
            return publishingDate ?? "";
        }

        public string getDescription()
        {
            var description = (string)bookData["items"][0]["volumeInfo"]["description"];
            return description ?? "";
        }

        public string getISBN10()
        {
            var isbn_10 = (string)bookData["items"][0]["volumeInfo"]["industryIdentifiers"][1]["identifier"];
            return isbn_10 ?? "";
        }

        public string getISBN13()
        {
            var isbn_13 = (string)bookData["items"][0]["volumeInfo"]["industryIdentifiers"][0]["identifier"];
            return isbn_13 ?? "";
        }

        public int getNumberOfPages()
        {
            var numberOfPages = (int?)bookData["items"][0]["volumeInfo"]["pageCount"];
            return numberOfPages != null ? (int)numberOfPages:0;
        }

        public string getPrintType() {
            var printType = (string)bookData["items"][0]["volumeInfo"]["printType"];
            return printType ?? "";
        }

        public string getMainCategory()
        {
            var mainCategory = (string)bookData["items"][0]["volumeInfo"]["mainCategory"];
            return mainCategory ?? "";
        }

        public List<string> getCategories()
        {
            JArray categories = ((JArray)bookData["items"][0]["categories"]);
            return categories.Any() ? categories.Select(a => (string)a).ToList() : new List<string>();
        }

        public float getAverageRating()
        {
            var averageRating = (float?)bookData["items"][0]["volumeInfo"]["averageRating"];
            return averageRating != null? (float)averageRating : 0.0f;
        }

        public int getRatingsCount()
        {
            var ratingsCount = (int?)bookData["items"][0]["volumeInfo"]["ratingsCount"];
            return ratingsCount != null ? (int)ratingsCount : 0;
        }

        public string getMaturityRating() { 
            var maturityRating = (string)bookData["items"][0]["volumeInfo"]["maturityRating"];
            return maturityRating ?? "";
        }

        public string getContentVersion()
        {
            var contentVersion = (string)bookData["items"][0]["volumeInfo"]["contentVersion"];
            return contentVersion ?? "";
        }

        public string getThumbnailImage()
        {
            string image = (string)bookData["items"][0]["volumeInfo"]["imageLinks"]["thumbnail"];
            return image ?? "";
        }

        public string getSmallThumbnailImage()
        {
            string image = (string)bookData["items"][0]["volumeInfo"]["imageLinks"]["smallThumbnail"];
            return image ?? "";
        }

        public string getSmallImage()
        {
            string image = (string)bookData["items"][0]["volumeInfo"]["imageLinks"]["small"];
            return image ?? "";
        }

        public string getMediumImage()
        {
            string image = (string)bookData["items"][0]["volumeInfo"]["imageLinks"]["medium"];
            return image ?? "";
        }

        public string getLargeImage()
        {
            string image = (string)bookData["items"][0]["volumeInfo"]["imageLinks"]["large"];
            return image ?? "";
        }

        public string getExtraLargeImage()
        {
            string image = (string)bookData["items"][0]["volumeInfo"]["imageLinks"]["extraLarge"];
            return image ?? "";
        }

        public string getLanguage()
        {
            var language = (string)bookData["items"][0]["volumeInfo"]["language"];
            return language ?? "";
        }

        public string getPreviewLink()
        {
            var previewLink = (string)bookData["items"][0]["volumeInfo"]["previewLink"];
            return previewLink ?? "";
        }

        public string getInfoLink()
        {
            var infoLink = (string)bookData["items"][0]["volumeInfo"]["infoLink"];
            return infoLink ?? "";
        }

        public string getCanonicalVolumeLink()
        {
            var CanonicalVolumeLink = (string)bookData["items"][0]["volumeInfo"]["canonicalVolumeLink"];
            return CanonicalVolumeLink ?? "";
        }

        public string getSearchInfo()
        {
            var searchInfo = (string)bookData["items"][0]["searchInfo"]["textSnippet"];
            return searchInfo ?? "";
        }

    }
}