'use client'

import apiClient from "./api";
import Image from "next/image";
import IterationIcon  from './components/IterationIcon'
import { useState, useEffect } from "react";

interface Image {
  id: number,
  title: string,
  description: string,
  url: string,
  likeCount: number,
  dislikeCount: number

}

const Home = () => {

  const [image, setImage] = useState<Image | null>(null);
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

  const handleClickIteration = (icon: string) => {
    alert(icon)
  }

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
            <div className="w-full">
              <div className="flex justify-between">
                <strong>{ image.title }</strong>
                <small>{ image.likeCount } likes</small>
              </div>
              <p className="text-sm text-justify text-slate-600">{ image.description }</p>
            </div>
            <Image
              src={ image.url } 
              alt="Image Title"
              width={600}
              height={400}
            />
            <div className="w-full justify-center flex gap-4 -translate-y-11">
              <IterationIcon icon="cancel" onClick={handleClickIteration} />
              <IterationIcon icon="favourite" onClick={handleClickIteration} />
            </div>
          </main>
        )
      }
    </div>
  );
  
}

export default Home;