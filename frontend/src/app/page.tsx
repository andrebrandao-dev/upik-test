'use client'

import apiClient from "./api";
import ImageView from "./components/ImageView";
import { useState, useEffect } from "react";
import { ImageType } from '@/app/types/imagetype'

const Home = () => {

  const [image, setImage] = useState<ImageType | null>(null);
  const [error, setError] = useState<Error | null>(null);

  useEffect(() => {
    const fetchImage = async () => {
      try {
        const { data } = await apiClient.get('/Image/1')
        setImage(data)
      } catch (err) {
        setError(err instanceof Error ? err : new Error(String(err)));
      }
    }

    fetchImage();
  }, []);
  
  if(error) {
    return (
      <div>
        Error
      </div>
    )
  }
  return (
    <div className="grid grid-rows-[20px_1fr_20px] items-center justify-items-center min-h-screen p-8 pb-20 gap-8">
      <h1>Match Upik</h1>
      {
        image && (
          <main className="flex flex-col row-start-2 gap-4 items-center sm:items-start">
            <ImageView image={ image } />
          </main>
        )
      }
    </div>
  );
}

export default Home;