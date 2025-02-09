const express = require('express');
const axios = require('axios');
   
const app = express();
const port = process.env.PORT || 3000;

// Endpoint של GET שמחזיר את רשימת האפליקציות המותקנות ב-Render
app.get('/apps', async (req, res) => {
  try {
    // ה-API Key שלך
    const apiKey = 'rnd_ZVVg1clgV1pEZISoaLbK5qELLE8P';

   
    const response = await axios.get('https://api.render.com/v1/services', {
      headers: {
        'Authorization': `Bearer ${apiKey}`,
      },
    });

   
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching data from Render API:', error);
    res.status(500).send('Error fetching data');
  }
});


app.listen(port, () => {
  console.log(`Server is running on port ${port}`);
});
