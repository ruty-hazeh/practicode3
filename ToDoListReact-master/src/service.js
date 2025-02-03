import axios from 'axios';
axios.defaults.baseURL = "http://localhost:5095";
axios.defaults.headers.common['Content-Type'] = 'application/json';


axios.interceptors.response.use(
  (response) => response, // מחזיר את התגובה אם אין שגיאה
  (error) => {
    console.error("API error:", error.message); // רישום שגיאה ללוג
    return Promise.reject(error); // החזרת השגיאה לטיפול נוסף אם נדרש
  }
);

export default {
  // שליפת כל המשימות
  getTasks: async () => {
      const result = await axios.get("/items");
      return result.data;
  },

  // הוספת משימה חדשה
  addTask: async (name) => {
      const response = await axios.post("/items", { name:name, isComplete: false });
      return response.data;
  },

 // עדכון סטטוס ה-completion של משימה (ועדכון שם אם צריך)
setCompleted: async (id, name, isComplete) => {
  if (!id || name === undefined || isComplete === undefined) {
    console.error("Missing parameters:", { id, name, isComplete });
    return;
  }

  const todoToUpdate = {
    name: name,      // שם המשימה
    isComplete: isComplete, // סטטוס ההשלמה
  };

  try {
    const result = await axios.put(`/items/${id}`, todoToUpdate);
    return result.data;
  } catch (error) {
    console.error('Error updating todo:', error);
    throw error;
  }
},// מחיקת משימה
  deleteTask: async (id) => {
    console.log('deleteTask')
      const response = await axios.delete(`/items/${id}`);
      return response.data;
  },
};